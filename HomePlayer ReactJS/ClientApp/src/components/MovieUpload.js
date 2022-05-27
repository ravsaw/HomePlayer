import { Button } from 'bootstrap';
import React, { Component } from 'react';

export class MovieUpload extends Component {
    static displayName = MovieUpload.name;

    constructor(props) {
        super(props);

    }

    componentDidMount() {

    }

    render() {

        return (
            <div>
                <h1 id="tabelLabel">Movies list</h1>
                <p>This component demonstrates movies list from the server.</p>

                <form method="post" action="/movieslist" enctype="multipart/form-data">
                    <input name='title' type='text' />
                    <br/>
                    <input name='upload' id='moviefile' type='file' accept='video/*'/>
                    <br/>
                    <input type="submit" value="Upload" />
                </form>
            </div>
        );
    }

}
