import React, { Component } from 'react';
//Babel plugin to not import the whole line chart package.
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

export default class Example extends Component<any, any> {
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
					<LineChart data={chartData}>
						<CartesianGrid strokeDasharray="3 3" />
						<XAxis dataKey="fetchTime" />
						<YAxis />
						<Tooltip />
						<Legend />
						<Line
							activeDot={{ dataKey: 'score', onClick: handleClick }}
							type="monotone"
							dataKey="score"
							stroke="#8884d8"
						/>
					</LineChart>
				</ResponsiveContainer>
			)
		);
	}
}
