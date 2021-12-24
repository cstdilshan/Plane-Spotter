import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import DatePicker from "react-datepicker";
import { Redirect } from 'react-router-dom';

import { userActions } from '../_actions';


class SpotterUpdatePage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            spotterObj: {
                make: '',
                model: '',
                registration: '',
                location: '',
                dateTime: new Date()
                //image : undefined
            },
            submitted: false,
            previewImage: undefined,
            IsMakeValid :true,
            IsModelValid :true,
            IsRegistrationValid :true,
            IsLocationValid :true,
            IsDateValid :true
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.dateChanged = this.dateChanged.bind(this);
        this.validateField = this.validateField.bind(this);
        //this.selectFile = this.selectFile.bind(this);
    }

    componentDidMount() {
        const { spotter  } = this.props;
        if(spotter == undefined)
        {
            this.props.history.push('/');
            return;
        }
        this.setState({
            spotterObj: spotter
        });
    }

    handleChange(event) {
        const { name, value } = event.target;
        this.validateField(name,value);
    }

    handleSubmit(event) {
        event.preventDefault();

        this.setState({ submitted: true });
        const {IsMakeValid,IsModelValid,IsRegistrationValid,IsLocationValid, spotterObj } = this.state;
        const { dispatch } = this.props;
        if (IsMakeValid && IsModelValid  && IsLocationValid && IsRegistrationValid ) {
            dispatch(userActions.updateSpotter(spotterObj));
        }
    }

    dateChanged(d){
        console.log(d);
        const { spotterObj } = this.state;
        this.setState({
            spotterObj: {
                        ...spotterObj,
                        dateTime: d
                    }
                });
      }

    validateField(fieldName, value) {
    const { IsMakeValid,IsModelValid,IsRegistrationValid,IsLocationValid, spotterObj } = this.state;
    let makeValid = IsMakeValid;
    let modelValid = IsModelValid;
    let registrationValid = IsRegistrationValid;
    let locationValid =IsLocationValid;

    var registrtionRegEx = new RegExp('^[a-zA-Z0-9]{1,2}-[a-zA-Z0-9]{1,5}');

    switch(fieldName) {
        case 'make':
            makeValid = value.length !=0 && value.length <= 128 ;
            break;
        case 'model':
            modelValid = value.length !=0 && value.length <= 128;
            break;
        case 'registration':
            registrationValid =  registrtionRegEx.test(value);
            break;
        case 'location':
            locationValid = value.length !=0 && value.length <= 255;
            break;
        default:
        break;
    }
    this.setState({
        spotterObj: {
            ...spotterObj,
            [fieldName]: value
        },
        IsMakeValid : makeValid,
        IsModelValid : modelValid,
        IsRegistrationValid : registrationValid,
        IsLocationValid : locationValid
    });

    //this.setState({IsMakeValid: makeValid});
    }

    render() {
        
        const { spotter  } = this.props;
        if (spotter == undefined) {
            return <Redirect to='/' />
          }
        const { spotterObj, submitted, previewImage, IsMakeValid,IsModelValid,IsRegistrationValid,IsLocationValid } = this.state;
        return (
            <div className="col-md-6 col-md-offset-3">
                <h2>Update Plane</h2>
                <form name="form" onSubmit={this.handleSubmit}>
                    <div className={'form-group' + (submitted && !IsMakeValid ? ' has-error' : '')}>
                        <label htmlFor="Make">Make</label>
                        <input type="text" className="form-control" name="make" value={spotterObj.make} onChange={this.handleChange} />
                        {submitted && !spotterObj.make &&
                            <div className="help-block">Make is required</div>
                        }
                    </div>
                    <div className={'form-group' + (submitted && !IsModelValid ? ' has-error' : '')}>
                        <label htmlFor="model">Model</label>
                        <input type="text" className="form-control" name="model" value={spotterObj.model} onChange={this.handleChange} />
                        {submitted && !spotterObj.model &&
                            <div className="help-block">Model is required</div>
                        }
                    </div>
                    <div className={'form-group' + (submitted && !IsRegistrationValid ? ' has-error' : '')}>
                        <label htmlFor="registration">Registration</label>
                        <input type="text" className="form-control" name="registration" value={spotterObj.registration} onChange={this.handleChange} />
                        {submitted && !IsRegistrationValid &&
                            <div className="help-block">Registration is required</div>
                        }
                    </div>
                    <div className={'form-group' + (submitted && !IsLocationValid ? ' has-error' : '')}>
                        <label htmlFor="location">Location</label>
                        <input type="text" className="form-control" name="location" value={spotterObj.location} onChange={this.handleChange} />
                        {submitted && !IsLocationValid &&
                            <div className="help-block">Location is required</div>
                        }
                    </div>
                    <div className={'form-group'}>
                        <label htmlFor="location">Date Time</label>
                        <div>
                        <DatePicker  className="form-control " selected={new Date(spotterObj.dateTime) } 
                        onChange={this.dateChanged}
                            showMonthDropdown={true}showYearDropdown={true}scrollableYearDropdown={false}/>
                        </div>
                        
                    </div>

                    <div className="form-group">
                        <button className="btn btn-primary">Register</button>
                        {/* {spotterRegistering && 
                            <img src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==" />
                        } */}
                        <Link to="/" className="btn btn-link">Back</Link>
                    </div>
                </form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { spotterUpdate } = state;
    const { spotter } = spotterUpdate;
    return {
        spotter
    };
}

const connectedSpotterUpdatePage = connect(mapStateToProps)(SpotterUpdatePage);
export { connectedSpotterUpdatePage as SpotterUpdatePage };