import React from "react";
import { Redirect, Route } from "react-router-dom";
import { ConnectedRouter } from "connected-react-router";
import { connect } from "react-redux";
// import App from "./containers/App/App";
import asyncComponent from "./helpers/AsyncFunc";
import TrashMap from "./customApp/TrashMap";
const PublicRoutes = ({ history, isLoggedIn }) => {
  return (
    <ConnectedRouter history={history}>
      <div>
        <Route exact path={"/"} component={TrashMap} />
      </div>
    </ConnectedRouter>
  );
};

export default connect((state) => ({
  isLoggedIn: false,
  //da dang nhap khi co reduce idToken hoac co localStore
}))(PublicRoutes);
