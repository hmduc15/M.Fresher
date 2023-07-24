<template>
  <el-tooltip
    class="tooltip"
    :content="content"
    :placement="placement"
    :effect="effect"
    :hide-after="0"
  >
    <slot name="child"></slot>
  </el-tooltip>
</template>

<script>
import { ElTooltip } from "element-plus";

export default {
  name: "MTooltip",
  props: ["placement", "content", "className", "effect", "delay"],
  component: {
    "el-tooltip": ElTooltip,
  },
  data() {
    return {};
  },
};
</script>

<style>
@import url(@/css/base/tooltip.css);
</style>
