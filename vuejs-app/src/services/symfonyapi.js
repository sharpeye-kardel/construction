/**
 * @file symfonyapi.js
 * @description Service module for communicating with the Symfony API backend.
 * Handles read-only operations for construction projects (list and detail retrieval).
 * Uses axios for HTTP requests with base URL configured via environment variable.
 */

import axios from 'axios';

/**
 * Axios instance configured for the Symfony API.
 * Base URL is set from VITE_APP_BASE_URL_SYMFONY environment variable.
 * @type {import('axios').AxiosInstance}
 */
const symfonyApi = axios.create({
  baseURL: import.meta.env.VITE_APP_BASE_URL_SYMFONY
});

/**
 * Retrieves all construction projects from the API.
 * @returns {Promise<import('axios').AxiosResponse>} The API response with hydra:member array of projects
 */
export const getApiData = () => {
  return symfonyApi.get('/constructions');
};

/**
 * Retrieves a specific construction project by ID.
 * @param {number|string} id - The unique identifier of the project to retrieve
 * @returns {Promise<import('axios').AxiosResponse>} The API response with project details
 */
export const getApiDataById = id => {
  return symfonyApi.get(`/constructions/${id}`);
};
