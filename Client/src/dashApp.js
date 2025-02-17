import React from "react";
import { Provider } from "react-redux";
import { store, history } from "./redux/store";
import PublicRoutes from "./router";
import { ThemeProvider } from "styled-components";
import { IntlProvider } from "react-intl";
import themes from "./settings/themes";

import { themeConfig } from "./settings";

import "./index.css";
const DashApp = () => (
  <IntlProvider locale={"en"}>
    <ThemeProvider theme={themes[themeConfig.theme]}>
      <Provider store={store}>
        <PublicRoutes history={history} />
      </Provider>
    </ThemeProvider>
  </IntlProvider>
);

export default DashApp;
