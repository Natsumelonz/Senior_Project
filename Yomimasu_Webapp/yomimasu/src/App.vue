<template>
  <v-app>
    <menubar/>
    <v-content>
      <router-view/>
    </v-content>
  </v-app>
</template>

<script>
import menubar from './components/menubar';

export default {
  name: 'App',
  components: {
    menubar
  },
  data: () => ({
    //
  }),
};
</script>
