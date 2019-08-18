import React, { Component } from 'react';
import './App.css';
import LineChart from './LineChart';

class App extends Component {
	state = { reports: [], reportPerfMetrics: [] };

	async componentDidMount() {
		const url = `${process.env.REACT_APP_API_ROOT}api/lighthouse/reports/1`;
		const reportsResponse = await fetch(url);
		const reports = await reportsResponse.json();

		this.setState({ reports });
	}

	handleClick = async (data: any, index: any) => {
		console.log(data.payload.id);
		const reportId = data.payload.id;
		const url = `${process.env.REACT_APP_API_ROOT}api/lighthouse/perfmetrics/${reportId}`;
		const perfMetricsResponse = await fetch(url);
		const reportPerfMetrics = await perfMetricsResponse.json();
		console.log(reportPerfMetrics);
		this.setState({ reportPerfMetrics });
	};
	render() {
		const { reports } = this.state;
		return (
			<div className="App">
				<header className="App-header">
					<LineChart handleClick={this.handleClick} reports={reports} />
				</header>
			</div>
		);
	}
}

export default App;
