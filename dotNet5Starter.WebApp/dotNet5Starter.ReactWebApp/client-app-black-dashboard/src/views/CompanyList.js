/*!

=========================================================
* Black Dashboard React v1.1.0
=========================================================

* Product Page: https://www.creative-tim.com/product/black-dashboard-react
* Copyright 2020 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/black-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
import React from "react";

// reactstrap components
import {
  Card,
  CardHeader,
  CardBody,
  CardTitle,
  Table,
  Row,
  Col
} from "reactstrap";

class CompanyList extends React.Component {

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
             <>
        <div className="content">
          <Row>
            <Col md="12" >
              <Card>
                <CardHeader>
                  <CardTitle tag="h4">Simple Table</CardTitle>
                </CardHeader>
                <CardBody>
                  <Table className="tablesorter" responsive>
                    <thead className="text-primary">
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
                  </Table>
                </CardBody>
              </Card>
            </Col>           
          </Row>
        </div>
      </> 
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : CompanyList.renderCompanyListTable(this.state.companies);

        return (contents

        );
    }

    async populateCompanyListData() {
        const response = await fetch('../api/companies');
        const data = await response.json();
        this.setState({ companies: data, loading: false });
    }
}
 

export default CompanyList;
