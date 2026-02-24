/**
 * @file App.vue
 * @description Root application component for the CPMS (Construction Project Management System).
 * Provides the main application layout with app bar and router view.
 */
<template>
  <v-app>
    <v-app-bar app>
      <v-toolbar-title>CPMS - PT ABC ABC</v-toolbar-title>
    </v-app-bar>
    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script>
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
}
</script>
