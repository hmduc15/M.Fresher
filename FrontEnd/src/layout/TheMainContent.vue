<template>
  <PropertyList></PropertyList>
</template>

<script>
import PropertyList from "@/view/property/PropertyList.vue";

export default {
  name: "TheMainConTent",
  data() {
    return {
      isShowForm: false,
    };
  },
  components: {
    PropertyList,
  },
  methods: {},
};
</script>

<style>
@import "@/css/base/content.css";
</style>
