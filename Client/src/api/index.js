import axios from "axios";

import { message, Modal } from "antd";
import { SECRETKEY } from "../settings/constants";
import moment from "moment";

const getConfig = async (headers = {}) => {
  if (!headers) headers = {};
  return {
    ...headers,
    Authorization: ``,
  };
};

const getConfigKey = async (headers = {}) => {
  return {
    ...headers,
    Authorization: `Bearer ${SECRETKEY}`,
  };
};

export { getConfig };

const callApi = (
  url,
  data = null,
  headers = {},
  method = "GET",
  responseType = "json"
) => {
  if (!headers) headers = {};

  headers = {
    Accept: "application/json",
    "Content-Type": "application/json",
    ...headers,
  };

  let params = {};
  if (typeof data === "object") {
    if (data instanceof FormData) {
      let key;
      for (key of data.keys()) {
        if (data.get(key) === "null" || data.get(key) === "undefined") {
          data.set(key, "");
        }
      }
      headers["Content-Type"] = "multipart/form-data";
    } else {
      let prop_name;
      for (prop_name in data) {
        if (data[prop_name] === null || data[prop_name] === undefined) {
          data[prop_name] = "";
        }
      }
    }
  }

  if (!(method === "PUT" || method === "POST" || method === "PATCH")) {
    params = data;
    data = {};
  }

  return axios({
    method,
    url,
    data,
    params,
    headers,
    responseType: responseType,
  })
    .then(function (response) {
      // if(response.data.Status && response.data.Status === -1 && headers.Authorization){ //het han token
      //   clearToken();
      //   store.dispatch({type: auth_actions.LOGOUT});
      //   history.replace('/signin');
      // }
      return response;
    })
    .catch(function (error) {
      if (error.response) {
        const statusText = error.response.Message
          ? error.response.Message
          : "Lỗi hệ thống";
        // The request was made and the server responded with a status code
        const status = error?.response?.status;
        if (status === 401) {
          auth_actions.logout();
          message.error(statusText);
          return error.response;
          // return;
        }
        // environment should not be used
        if (status === 403) {
          message.error(statusText);
          return error.response;
          // history.push('404');
          // return error.response;
        }
        if (status <= 504 && status >= 500) {
          message.error(statusText);
          return error.response;
          // router.push('/exception/500');
          // return;
        }
        if (status >= 404 && status < 422) {
          message.error(statusText);
          return error.response;
          // history.push('404');
          // return;
        }
      } else if (error.request) {
        // The request was made but no response was received
        // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
        // http.ClientRequest in node.js
      } else {
        // Something happened in setting up the request that triggered an Error
      }
      //Error config
    });
};

export const apiGet = async (url, params = null, headers = {}) => {
  return await callApi(url, params, headers, "GET", "json");
};

export const apiPost = async (
  url,
  params = null,
  headers = {},
  type = "json"
) => {
  return await callApi(url, params, headers, "POST", type);
};

export const apiPut = async (url, params = null, headers = {}) => {
  return await callApi(url, params, headers, "PUT", "json");
};

export const apiPatch = async (url, params = null, headers = {}) => {
  return await callApi(url, params, headers, "PATCH", "json");
};

export const apiDelete = async (url, params = null, headers = {}) => {
  return await callApi(url, params, headers, "DELETE", "json");
};
