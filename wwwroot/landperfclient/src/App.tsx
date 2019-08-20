import React, { Component, Fragment } from 'react';
import './App.css';
import AreaChart from './AreaChart';
import PerfMetricCard from './PerfMetricCard';
import Select from './Select';
import { Report, PerfMetric, Url } from './interfaces';

interface State {
	reports: Report[];
	perfMetrics: PerfMetric[];
	urls: Url[];
	reportFetchTime: string;
}

class App extends Component<any, State> {
	state = { reports: [], perfMetrics: [], urls: [], reportFetchTime: '' };

	async componentDidMount() {
		const urlsUrl = `${process.env.REACT_APP_API_ROOT}api/lighthouse/urls`;
		const urlsResponse = await fetch(urlsUrl);
		const urls: Url[] = await urlsResponse.json();

		this.setState({ urls });
	}

	handleReportClick = async (data: any, index: any) => {
		const reportFetchTime = data.payload.fetchTime;
		const reportId = data.payload.id;
		const perfMetricsUrl = `${process.env.REACT_APP_API_ROOT}api/lighthouse/perfmetrics/${reportId}`;
		const perfMetricsResponse = await fetch(perfMetricsUrl);
		const perfMetrics: PerfMetric[] = await perfMetricsResponse.json();

		this.setState({ perfMetrics, reportFetchTime });
	};

	handleSelectClick = async (urlId: number) => {
		const url = `${process.env.REACT_APP_API_ROOT}api/lighthouse/reports/${urlId}`;
		const reportsResponse = await fetch(url);
		const reports: Report[] = await reportsResponse.json();

		this.setState({ reports, perfMetrics: [] });
	};

	render() {
		const { reports, perfMetrics, urls, reportFetchTime } = this.state;
		return (
			<div className="App">
				<header className="Header">LandPerf</header>
				<Select urls={urls} handleClick={this.handleSelectClick} />
				{reports && reports.length > 0 && (
					<div className="chart-container">
						<AreaChart handleClick={this.handleReportClick} reports={reports} />
					</div>
				)}

				{perfMetrics && perfMetrics.length > 0 && (
					<Fragment>
						{reportFetchTime && <h3 className="report-timestamp-header">Report conducted on {reportFetchTime}</h3>}
						<div className="perf-metrics">
							{perfMetrics.map((perfMetric: PerfMetric) => (
								<PerfMetricCard
									key={`${perfMetric.title}${perfMetric.displayValue}`}
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
