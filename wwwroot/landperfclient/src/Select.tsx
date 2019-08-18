import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';

const useStyles = makeStyles(theme => ({
	formControl: {
		margin: theme.spacing(1),
		width: 500
	},
	label: {
		color: '#fff'
	},
	select: {
		color: '#fff'
	}
}));

function SimpleSelect(props: any) {
	const classes = useStyles();
	const [values, setValues] = React.useState({
		url: ''
	});

	function handleChange(event: any) {
		const url = event.target.name;
		const urlId = event.target.value;
		console.log(event.target.name, event.target.value);
		setValues(oldValues => ({
			...oldValues,
			[url]: urlId
		}));
		props.handleClick(urlId);
	}
	console.log(values);

	return (
		<FormControl className={classes.formControl}>
			<InputLabel className={classes.label} htmlFor="url">
				Url
			</InputLabel>
			<Select
				className={classes.select}
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
