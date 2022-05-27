import { Button } from 'bootstrap';
import React, { Component } from 'react';

export class MoviesList extends Component {
    static displayName = MoviesList.name;

    constructor(props) {
        super(props);
        this.state = { mList: [], loading: true };
    }

    componentDidMount() {
        this.populatMoviesList();
    }

    static renderMoviesTable(mList) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                    </tr>
                </thead>
                <tbody>
                    {mList.map(m =>
                        <tr key={m.title}>
                            <td>{m.title}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : MoviesList.renderMoviesTable(this.state.mList);

        return (
            <div>
                <h1 id="tabelLabel">Movies list</h1>
                <p>This component demonstrates movies list from the server.</p>
                {contents}
            </div>
        );
    }

    async populatMoviesList() {
        const response = await fetch('movieslist');
        const data = await response.json();
        this.setState({ mList: data, loading: false });
    }
}
