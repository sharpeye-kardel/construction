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
  name: 'App',

  data: () => ({
    //
  }),
  components: {
    LogoutButton
  },
}
</script>
