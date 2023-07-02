<template>
  <AssetList></AssetList>
</template>

<script>
import AssetList from '@/view/asset/AssetList.vue';

export default {
  name: "TheMainConTent",
  data() {
    return {
      isShowForm: false,
    };
  },
  components: {
    AssetList,
  },
  methods: {},
};
</script>

<style>
@import "@/css/base/content.css";
</style>
