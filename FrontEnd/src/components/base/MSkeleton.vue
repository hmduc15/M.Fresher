<template>
  <m-skeleton :loading="isLoading" animated :rows="rows"> </m-skeleton>
</template>

<script>
import { ElSkeleton } from "element-plus";

export default {
  name: "MSkelton",
  props: ["isLoading", "rows", "variant", "width"],
  components: {
    "m-skeleton": ElSkeleton,
  },
};
</script>
<style>
.loading__container {
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 0 8px;
}

.el-skeleton__p.is-first {
  width: 100% !important;
  height: 6px;
}
</style>
