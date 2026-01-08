/**
 * @file LogoutButton.vue
 * @description A button component that handles user logout functionality.
 * Dispatches the Vuex logout action and redirects the user to the login page.
 *
 * @example
 * <LogoutButton />
 */
<template>
  <v-btn @click="logout" color="error">Logout</v-btn>
</template>

<script>
import { mapActions } from 'vuex';

export default {
  methods: {
    /**
     * Maps the Vuex 'logoutTo' action to the local 'logoutAction' method.
     * @function logoutAction
     * @returns {void}
     */
    ...mapActions({logoutAction: 'logoutTo'}),
    /**
     * Logs out the current user by dispatching the Vuex logout action
     * and redirecting to the login page.
     * @returns {void}
     */
    logout() {
      this.logoutAction();
      this.$router.push('/login');
    }
  }
};
</script>
