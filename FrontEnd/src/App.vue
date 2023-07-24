<template>
  <div class="container">
    <m-sidebar></m-sidebar>
    <div :class="[`${!isCollapsed ? 'main' : 'main--active'}`]">
      <m-header></m-header>
      <router-view />
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

import TheSidebar from "./layout/TheSidebar.vue";
import TheMainHeader from "./layout/TheMainHeader.vue";

export default {
  name: "App",
  components: {
    "m-sidebar": TheSidebar,
    "m-header": TheMainHeader,
  },

  computed: {
    ...mapState("sideBar", ["isCollapsed"]),
  },
};
</script>
<style scoped>
@import "@/css/main.css";
@import "@/css/base/layout.css";
</style>
