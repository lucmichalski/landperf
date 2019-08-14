const lighthouse = require('lighthouse');
const puppeteer = require('puppeteer');

async function launchPuppeteerRunLighthouse(url) {
  try {
    const args = [
      '--no-sandbox',
      '--incognito',
      '--disable-gpu',
      '--disable-dev-shm-usage',
      '--disable-setuid-sandbox',
      '--no-first-run',
      '--no-zygote'
    ];
    const browser = await puppeteer.launch({
      args
    });
    const config = {
      extends: 'lighthouse:default',
      settings: {
        maxWaitForFcp: 30 * 1000,
        onlyAudits: [
          'first-meaningful-paint',
          'interactive',
          'first-contentful-paint',
          'first-cpu-idle',
          'estimated-input-latency',
          'speed-index'
        ]
      }
    };

    const port = browser._connection._url.slice(15, 20);
    const { lhr, report } = await lighthouse(
      url,
      {
        port
      },
      config
    );
    browser.close();

    if ((lhr && lhr.runtimeError && lhr.runtimeError.code === 'NO_ERROR') || (lhr && !lhr.runtimeError)) {
      console.log(lhr.requestedUrl, lhr.categories.performance.score);
      console.log(lhr, 'lhr');
      return lhr;
    }

    return {};
  } catch (error) {
    console.log(error);
    throw error;
  }
}

module.exports = async function(callback, url) {
  try {
    const lhr = await launchPuppeteerRunLighthouse(url);
    callback(/* error */ null, lhr);
  } catch (error) {
    callback(error, null);
  }
};
