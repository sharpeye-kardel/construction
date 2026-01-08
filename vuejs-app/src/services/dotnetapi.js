/**
 * @file dotnetapi.js
 * @description Service module for communicating with the .NET API backend.
 * Handles construction project CRUD operations (create, update, delete) and user authentication.
 * Uses axios for HTTP requests with base URL configured via environment variable.
 */

import axios from 'axios';

/**
 * Axios instance configured for the .NET API.
 * Base URL is set from VITE_APP_BASE_URL_DOTNET environment variable.
 * @type {import('axios').AxiosInstance}
 */
const dotnetApi = axios.create({
  baseURL: import.meta.env.VITE_APP_BASE_URL_DOTNET
});

/**
 * Creates a new construction project.
 * @param {Object} data - The construction project data
 * @param {string} data.name - Project name
 * @param {string} data.location - Project location
 * @param {number} data.stage - Project stage (1-4)
 * @param {string} data.category - Project category
 * @param {string} data.details - Project details
 * @param {string} data.startDate - Project start date in ISO format
 * @returns {Promise<import('axios').AxiosResponse>} The API response with created project
 */
export const createApiData = data => {
  return dotnetApi.post('/constructions', data);
};

/**
 * Updates an existing construction project.
 * @param {number|string} id - The unique identifier of the project to update
 * @param {Object} data - The updated construction project data
 * @param {string} data.name - Project name
 * @param {string} data.location - Project location
 * @param {number} data.stage - Project stage (1-4)
 * @param {string} data.category - Project category
 * @param {string} data.details - Project details
 * @param {string} data.startDate - Project start date in ISO format
 * @returns {Promise<import('axios').AxiosResponse>} The API response with updated project
 */
export const updateApiData = (id, data) => {
  return dotnetApi.put(`/constructions/${id}`, data);
};

/**
 * Deletes a construction project.
 * @param {number|string} id - The unique identifier of the project to delete
 * @returns {Promise<import('axios').AxiosResponse>} The API response confirming deletion
 */
export const deleteApiData = (id) => {
  return dotnetApi.delete(`/constructions/${id}`);
};

/**
 * Authenticates a user and retrieves a JWT token.
 * @param {Object} data - The login credentials
 * @param {string} data.email - User's email address
 * @param {string} data.password - User's password
 * @returns {Promise<import('axios').AxiosResponse>} The API response with JWT token
 */
export const loginApiData = (data) => {
  console.log("base url is ", import.meta.env.VITE_APP_BASE_URL_DOTNET);
  return dotnetApi.post(`/users/login`, data);
};

