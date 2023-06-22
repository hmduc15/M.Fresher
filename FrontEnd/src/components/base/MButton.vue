<template>
  <m-tooltip
    v-if="title"
    :content="title"
    :placement="posTooltip"
    :effect="isEffect"
  >
    <template #child>
      <button :type="type" :class="className" @click.stop="$emit('click')">
        <div :class="iconButton"></div>
      </button>
    </template>
  </m-tooltip>
  <button v-else :type="type" :class="className">
    <div :class="iconButton"></div>
    <div v-if="content" class="text__btn">
      {{ content }}
    </div>
  </button>
</template>

<script>
import MTooltip from "./MTooltip.vue";

export default {
  name: "MButton",
  props: [
    "className",
    "iconButton",
    "content",
    "title",
    "type",
    "posTooltip",
    "isEffect",
  ],
  components: {
    "m-tooltip": MTooltip,
  },
};
</script>

<style>
@import "@/css/base/button.css";
.text__btn {
  display: none;
}
</style>
