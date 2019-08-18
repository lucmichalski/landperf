import React, { Component } from 'react';
//Babel plugin to not import the whole line chart package.
import { AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import './AreaChart.css';

const CustomTooltip = (props: any) => {
	if (props.active) {
		const { fetchTime, score, id } = props.payload[0].payload;
		return (
			<div className="custom-tooltip">
				<div className="tooltip-report-id">Report Id: {id}</div>
				<div className="tooltip-report-score">Performance Overall Score: {score}</div>
				<div className="tooltip-report-date">Fetch Time: {fetchTime}</div>
			</div>
		);
	}

	return null;
};

export default class CustomAreaChart extends Component<any, any> {
	render() {
		const { reports, handleClick } = this.props;

		const chartData =
			reports &&
			reports.length > 0 &&
			reports.map((report: any) => ({
				fetchTime: report.fetchTime,
				score: report.performance,
				id: report.id
			}));

		return (
			chartData &&
			chartData.length > 0 && (
				<ResponsiveContainer width={'100%'} height="80%">
					<AreaChart margin={{ top: 5, right: 30, left: 20, bottom: 5 }} data={chartData}>
						<CartesianGrid stroke="rgba(0,0,0,.2)" />
						<XAxis />
						<YAxis />
						<Tooltip content={<CustomTooltip />} />
						<Legend verticalAlign="top" height={36} />
						<Area stroke="#ef0078" fill="#ef0078" activeDot={{ dataKey: 'score', onClick: handleClick }} type="monotone" dataKey="score" />
					</AreaChart>
				</ResponsiveContainer>
			)
		);
	}
}
