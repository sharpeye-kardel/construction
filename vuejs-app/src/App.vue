/**
 * @file App.vue
 * @description Root application component for the CPMS (Construction Project Management System).
 * Provides the main application layout with app bar, logout functionality, and router view.
 *
 * @uses LogoutButton - Displays logout button when user is authenticated
 */
<template>
  <v-app>
    <v-app-bar app>
      <v-toolbar-title>CPMS - PT ABC ABC</v-toolbar-title>
      <LogoutButton v-if="$store.getters.isAuthenticated" />
    </v-app-bar>
    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script>
import LogoutButton from '@/components/LogoutButton.vue';

export default {
  /**
   * Component name identifier.
   */
  name: 'App',

  /**
   * @data
   * @returns {Object} Empty data object - no reactive data needed for root component
   */
  data: () => ({
    //
  }),
  /**
   * Child components used in this component.
   */
  components: {
    LogoutButton
  },
}
</script>
