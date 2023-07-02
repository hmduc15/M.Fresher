<template>
  <div class="container">
    <m-sidebar></m-sidebar>
    <div :class="[`${!isCollapsed ? 'main' : 'main--active'}`]">
      <m-header></m-header>
      <m-content></m-content>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

import TheSidebar from "./layout/TheSidebar.vue";
import TheMainHeader from "./layout/TheMainHeader.vue";
import TheMainContent from "./layout/TheMainContent.vue";
export default {
  name: "App",
  components: {
    "m-sidebar": TheSidebar,
    "m-header": TheMainHeader,
    "m-content": TheMainContent,
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
