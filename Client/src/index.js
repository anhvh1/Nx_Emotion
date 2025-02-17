import React from "react";
import ReactDOM from "react-dom";
import DashApp from "./dashApp";
import registerServiceWorker from "./registerServiceWorker";
import { Router as BrowserRouter } from "react-router-dom";
import { createRoot } from "react-dom/client";
import "antd/dist/reset.css";
import { emptyCache } from "./helpers/utility";

const container = document.getElementById("root");
const root = createRoot(container);
root.render(<DashApp />);
// Hot Module Replacement API
if (module.hot) {
  module.hot.accept("./dashApp.js", () => {
    const NextApp = require("./dashApp").default;
    root.render(<NextApp />);
    // ReactDOM.render(<NextApp />, document.getElementById("root"));
  });
}
registerServiceWorker();
emptyCache();
