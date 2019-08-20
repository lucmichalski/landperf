export interface Url {
	id: number;
	name: string;
	siteId: number;
}

export interface Report {
	id: number;
	fetchTime: string;
	urlId: number;
	performance: number;
}

export interface PerfMetric {
	displayValue: string;
	numericValue?: number;
	reportId?: number;
	score: number;
	title: string;
}
