<template>
  <v-dialog content-class="ma-0" persistent lazy max-width="700" :value="dialog">
    <v-card v-if="suggestions.length > 0">
     <v-layout align-center wrap>
        <v-flex grow>
          <div class="headline text-xs-center pt-4">{{suggestions[page-1].date}}</div>
          <div class="subheading text-xs-center">Bookable times</div>
        </v-flex>
        <v-flex shrink>
          <div class="close"><v-icon size="30" @click="$emit('close')">close</v-icon></div>
        </v-flex>
     </v-layout>
     <v-layout class="pa-5" wrap>
        <v-flex class="pa-3" md4 xs6 v-for="(startTime, index) in suggestions[page-1].startTimes" :key="index">
          <v-card>
            <v-card-text class="subheading text-xs-center font-weight-bold">{{startTime}}</v-card-text>
          </v-card>
        </v-flex>
     </v-layout>
     <v-flex class="text-xs-center pb-4">
        <v-pagination
        v-model="page"
        :length="suggestions.length"
        :total-visible="5"
        ></v-pagination>
     </v-flex>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    suggestions: {
      type: Array
    },
    dialog: {
      type: Boolean
    }
  },
  data () {
    return {
      page: 1
    }
  }
}
</script>

<style>
.close{
  position: absolute;
  top: 20px;
  right: 20px;
}
</style>
