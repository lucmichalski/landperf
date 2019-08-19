import React from 'react';
//Babel plugin to not import the whole line chart package.
import { AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import './AreaChart.css';

const CustomTooltip = (props: any) => {
	if (props.active) {
		const { fetchTime, score } = props.payload[0].payload;
		return (
			<div className="custom-tooltip">
				<div className="tooltip-report-score">Performance Overall Score: {score}</div>
				<div className="tooltip-report-date">Fetch Time: {fetchTime}</div>
			</div>
		);
	}

	return null;
};

export default function CustomAreaChart(props: any) {
	const { reports, handleClick } = props;
	//Only set ticks on x axis if month changes.
	let month = -1;
	const chartData = reports.map((report: any) => {
		const date = new Date(report.fetchTime);
		const newMonth = date.getMonth();
		if (newMonth !== month) {
			month = newMonth;
			return {
				fetchTime: report.fetchTime,
				score: report.performance,
				id: report.id,
				month: date.toLocaleString('default', { month: 'long' })
			};
		}
		return {
			fetchTime: report.fetchTime,
			score: report.performance,
			id: report.id,
			month: ''
		};
	});

	return (
		<ResponsiveContainer width={'100%'} height="80%">
			<AreaChart margin={{ top: 20, right: 30, left: 20, bottom: 50 }} data={chartData}>
				<CartesianGrid stroke="rgba(0,0,0,.2)" />
				<XAxis tick={{ transform: 'translate(0,20)' }} dataKey="month" />
				<YAxis />
				<Tooltip content={<CustomTooltip />} />
				<Area
					stroke="#ef0078"
					fill="#ef0078"
					activeDot={{ dataKey: 'score', onClick: handleClick }}
					type="monotone"
					dataKey="score"
				/>
			</AreaChart>
		</ResponsiveContainer>
	);
}
