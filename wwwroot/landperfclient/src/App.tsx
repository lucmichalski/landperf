import React, { Component, Fragment } from 'react';
import './App.css';
import AreaChart from './AreaChart';
import PerfMetric from './PerfMetric';
import Select from './Select';

interface State {
	reports: any[];
	reportPerfMetrics: any[];
	urls: any[];
	reportFetchTime: string;
}

class App extends Component<any, State> {
	state = { reports: [], reportPerfMetrics: [], urls: [], reportFetchTime: '' };

	async componentDidMount() {
		const urlsUrl = `${process.env.REACT_APP_API_ROOT}api/lighthouse/urls`;
		const urlsResponse = await fetch(urlsUrl);
		const urls = await urlsResponse.json();

		this.setState({ urls });
	}

	handleReportClick = async (data: any, index: any) => {
		const reportFetchTime = data.payload.fetchTime;
		const reportId = data.payload.id;
		const perfMetricsUrl = `${process.env.REACT_APP_API_ROOT}api/lighthouse/perfmetrics/${reportId}`;
		const perfMetricsResponse = await fetch(perfMetricsUrl);
		const reportPerfMetrics = await perfMetricsResponse.json();

		this.setState({ reportPerfMetrics, reportFetchTime });
	};

	handleSelectClick = async (urlId: number) => {
		const url = `${process.env.REACT_APP_API_ROOT}api/lighthouse/reports/${urlId}`;
		const reportsResponse = await fetch(url);
		const reports = await reportsResponse.json();

		this.setState({ reports, reportPerfMetrics: [] });
	};

	render() {
		const { reports, reportPerfMetrics, urls, reportFetchTime } = this.state;
		return (
			<div className="App">
				<header className="Header">LandPerf</header>
				<Select urls={urls} handleClick={this.handleSelectClick} />
				{reports && reports.length > 0 && (
					<div className="chart-container">
						<AreaChart handleClick={this.handleReportClick} reports={reports} />
					</div>
				)}

				{reportPerfMetrics && reportPerfMetrics.length > 0 && (
					<Fragment>
						{reportFetchTime && <h3 className="report-timestamp-header">Report conducted on {reportFetchTime}</h3>}
						<div className="perf-metrics">
							{reportPerfMetrics.map((perfMetric: any) => (
								<PerfMetric
									key={perfMetric.title}
									title={perfMetric.title}
									score={perfMetric.score}
									displayValue={perfMetric.displayValue}
								/>
							))}
							<a
								className={'lighthouse-link'}
								href="https://docs.google.com/spreadsheets/d/1Cxzhy5ecqJCucdf1M0iOzM8mIxNc7mmx107o5nj38Eo/edit#gid=0"
							>
								Lighthouse Scoring Weights
							</a>
						</div>
					</Fragment>
				)}
			</div>
		);
	}
}

export default App;
