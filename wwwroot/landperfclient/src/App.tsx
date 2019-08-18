import React, { Component } from 'react';
import './App.css';
import AreaChart from './AreaChart';
import PerfMetric from './PerfMetric';
import Select from './Select';

class App extends Component<any, any> {
	state = { reports: [], reportPerfMetrics: [], urls: [] };

	async componentDidMount() {
		const urlsUrl = `${process.env.REACT_APP_API_ROOT}api/lighthouse/urls`;
		const urlsResponse = await fetch(urlsUrl);
		const urls = await urlsResponse.json();

		this.setState({ urls });
	}

	handleReportClick = async (data: any, index: any) => {
		console.log(data.payload.id);
		const reportId = data.payload.id;
		const url = `${process.env.REACT_APP_API_ROOT}api/lighthouse/perfmetrics/${reportId}`;
		const perfMetricsResponse = await fetch(url);
		const reportPerfMetrics = await perfMetricsResponse.json();
		console.log(reportPerfMetrics);
		this.setState({ reportPerfMetrics });
	};

	handleSelectClick = async (urlId: number) => {
		const url = `${process.env.REACT_APP_API_ROOT}api/lighthouse/reports/${urlId}`;
		const reportsResponse = await fetch(url);
		const reports = await reportsResponse.json();

		this.setState({ reports, reportPerfMetrics: [] });
	};

	render() {
		const { reports, reportPerfMetrics, urls } = this.state;
		return (
			<div className="App">
				<header className="Header">LandPerf</header>
				<Select urls={urls} handleClick={this.handleSelectClick} />
				<div className="chart-container">
					<AreaChart handleClick={this.handleReportClick} reports={reports} />
				</div>
				<div className="perf-metrics">
					{reportPerfMetrics &&
						reportPerfMetrics.length > 0 &&
						reportPerfMetrics.map((perfMetric: any) => (
							<PerfMetric
								key={perfMetric.title}
								title={perfMetric.title}
								score={perfMetric.score}
								displayValue={perfMetric.displayValue}
							/>
						))}
				</div>
			</div>
		);
	}
}

export default App;
