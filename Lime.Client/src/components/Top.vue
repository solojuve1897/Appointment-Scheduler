<template>
  <v-toolbar app dark color="primary">
    <v-icon @click="$router.push({name: 'home'})" size="36">event</v-icon>
    <v-toolbar-title @click="$router.push({name: 'home'})" class="headline clickable">
      <span>Scheduler</span>
    </v-toolbar-title>
    <v-spacer></v-spacer>
    <v-btn @click="$router.push({name: 'about'})" icon>
      <v-icon size="28">help_outline</v-icon>
    </v-btn>
  </v-toolbar>
</template>

<script>
export default {
  name: 'Top'
}
</script>

<style>
</style>
