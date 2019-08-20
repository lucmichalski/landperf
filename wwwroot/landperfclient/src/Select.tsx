import React from 'react';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import { Url } from './interfaces';
import './Select.css';

interface Props {
	urls: Url[];
	handleClick: (urlId: number) => Promise<void>;
}
function SimpleSelect(props: Props) {
	const [values, setValues] = React.useState({
		url: ''
	});

	function handleChange(event: any) {
		const url = event.target.name;
		const urlId = event.target.value;

		setValues(oldValues => ({
			...oldValues,
			[url]: urlId
		}));
		props.handleClick(urlId);
	}

	return (
		<FormControl className={'formControl'}>
			<InputLabel className={'label'} htmlFor="url">
				Url
			</InputLabel>
			<Select
				className={'select'}
				value={values.url}
				onChange={handleChange}
				inputProps={{
					name: 'url',
					id: 'url'
				}}
			>
				{props.urls &&
					props.urls.length > 0 &&
					props.urls.map((url: any) => (
						<MenuItem key={url.id} value={url.id}>
							{url.name}
						</MenuItem>
					))}
			</Select>
		</FormControl>
	);
}

export default SimpleSelect;
