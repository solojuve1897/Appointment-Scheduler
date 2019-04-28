<template>
  <v-app>
    <top/>
    <v-content class="content">
      <router-view/>
    </v-content>
  </v-app>
</template>

<script>
import Top from '@/components/Top'
export default {
  name: 'App',
  components: {
    Top
  }
}
</script>

<style lang="scss">
.content{
  width: 100%;
  max-width: 1024px;
  margin: 0 auto;
  .v-content__wrap{
    background: #fff;
    padding: 40px 35px;
    @media (min-width: 1100px) {
      margin-top:50px;
      margin-bottom:50px;
      border-radius: 5px;
      border: 1px solid #ddd;
    }
  }
}
</style>
