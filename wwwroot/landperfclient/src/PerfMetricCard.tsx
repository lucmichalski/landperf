import React from 'react';
import Guage from './Guage';
import { PerfMetric } from './interfaces';
import './PerfMetricCard.css';

const PerfMetricCard = (props: PerfMetric) => {
	return (
		<div className="perf-metric">
			<Guage value={props.score} />
			<div>{props.title}</div>
			<div className={'score'}>{props.score}</div>
			<div>{props.displayValue}</div>
		</div>
	);
};

export default PerfMetricCard;
