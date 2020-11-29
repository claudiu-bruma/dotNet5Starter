import React, { Component } from 'react';

export class CompanyList extends Component {
    static displayName = CompanyList.name;

    constructor(props) {
        super(props);
        this.state = { companies: [], loading: true };
    }

    componentDidMount() {
        this.populateCompanyListData();
    }

    static renderCompanyListTable(companies) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Exchange</th>
                        <th>StockTicker</th>
                        <th>Isin</th>
                        <th>Website</th>
                    </tr>
                </thead>
                <tbody>
                    {companies.map(company =>
                        <tr key={company.id}>
                            <td>{company.name}</td>
                            <td>{company.exchange}</td>
                            <td>{company.stockTicker}</td>
                            <td>{company.isin}</td>
                            <td>{company.website}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : CompanyList.renderCompanyListTable(this.state.companies);

        return (
            <div>
                <h1 id="tabelLabel" >Company List</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateCompanyListData() {
        const response = await fetch('companies');
        const data = await response.json();
        this.setState({ companies: data, loading: false });
    }
}
