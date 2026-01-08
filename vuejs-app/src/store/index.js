/**
 * @file store/index.js
 * @description Vuex store for managing application state.
 * Handles user authentication state including login, logout, and token management.
 */

import { createStore } from 'vuex';
import { loginApiData as authLogin } from '@/services/dotnetapi';

/**
 * Creates and exports the Vuex store instance.
 * @type {import('vuex').Store}
 */
export default createStore({
  /**
   * @property {Object} state - The reactive state object
   * @property {string|null} state.user - The authenticated user's email, null if not logged in
   * @property {string|null} state.token - The JWT authentication token, null if not logged in
   */
  state: {
    user: null,
    token: null
  },
  /**
   * Mutations for synchronously updating state.
   */
  mutations: {
    /**
     * Sets the authenticated user.
     * @param {Object} state - The Vuex state
     * @param {string} user - The user's email address
     */
    setUser(state, user) {
      state.user = user;
    },
    /**
     * Sets the authentication token.
     * @param {Object} state - The Vuex state
     * @param {string} token - The JWT token
     */
    setToken(state, token) {
      state.token = token;
    },
    /**
     * Clears all authentication data (logout).
     * @param {Object} state - The Vuex state
     */
    clearAuthData(state) {
      state.user = null;
      state.token = null;
    }
  },
  /**
   * Actions for asynchronous operations.
   */
  actions: {
    /**
     * Authenticates a user with the .NET API.
     * @param {Object} context - The Vuex action context
     * @param {Function} context.commit - The commit function for mutations
     * @param {Object} authData - The authentication credentials
     * @param {string} authData.email - User's email address
     * @param {string} authData.password - User's password
     * @returns {Promise<void>}
     * @throws {Error} Throws an error if login fails
     */
    async loginTo({ commit }, authData) {
      try {
        const response = await authLogin(authData);
        const token = response.data.token;
        const user = authData.email;
        commit('setToken', token);
        commit('setUser', user);
      } catch (error) {
        throw new Error(error.response?.data?.message || 'Failed to login');
      }
    },
    /**
     * Logs out the current user by clearing authentication data.
     * @param {Object} context - The Vuex action context
     * @param {Function} context.commit - The commit function for mutations
     */
    logoutTo({ commit }) {
      commit('clearAuthData');
    }
  },
  /**
   * Getters for computed state properties.
   */
  getters: {
    /**
     * Checks if a user is currently authenticated.
     * @param {Object} state - The Vuex state
     * @returns {boolean} True if user has a valid token
     */
    isAuthenticated(state) {
      return state.token !== null;
    },
    /**
     * Gets the current authenticated user.
     * @param {Object} state - The Vuex state
     * @returns {string|null} The user's email or null if not authenticated
     */
    user(state) {
      return state.user;
    }
  }
})
