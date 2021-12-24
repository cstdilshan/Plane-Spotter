import React from 'react';
import { connect } from 'react-redux';

import { userActions } from '../_actions';
import DataTable from 'react-data-table-component';
//import { columns } from "./data";

class HomePage extends React.Component {

    componentDidMount() {
        this.props.dispatch(userActions.getAllSpotters());
    }

    handleDeleteUser(id) {
        return (e) => this.props.dispatch(userActions.delete(id));
    }

    handleEditSpotter(id) {
        return (e) => this.props.dispatch(userActions.getSpotterById(id));
    }
    // handleLogoutUser(id) {
    //     return (e) => this.props.dispatch(userActions.logout());
    // }
 


    render() {
        const { user, spotters } = this.props;
        const columns = [
            {
              name: "Make",
              selector: row => row.make,
              //sortable: true
            },
            {
              name: "Model",
              selector: row => row.model,
              //sortable: true
            },
            {
              name: "Registration",
              selector: row => row.registration,
              //sortable: true,
            },
            {
              name: "Location",
              selector: row => row.location,
              //sortable: true
            },
            {
                cell: row => (
                    <button className="btn btn-primary" onClick={this.handleEditSpotter(row.id)}>Edit</button>
                  )
            }
          ];
        return (
            <div className="col-md-12">
                <h2>Hi {user.firstName}</h2>
                {spotters.loading && <em>Loading spotters...</em>}
                {spotters.error && <span className="text-danger">ERROR: {spotters.error}</span>}

                <DataTable
                    title="Registerd Planes"
                    columns={columns}
                    data={spotters.items}
                    pagination
                    pointerOnHover
                />
                <p>
                    
                </p>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { spotters, authentication } = state;
    const { user } = authentication;
    
    return {
        user,
        spotters
    };
}

const connectedHomePage = connect(mapStateToProps)(HomePage);
export { connectedHomePage as HomePage };

