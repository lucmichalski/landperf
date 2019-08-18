import React from 'react';
import './PerfMetric.css';
const PerfMetric = (props: any) => {
	return (
		<div className="main">
			<div>{props.title}</div>
			<div>{props.score}</div>
			<div>{props.displayValue}</div>
		</div>
	);
};

export default PerfMetric;
