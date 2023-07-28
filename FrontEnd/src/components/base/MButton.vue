<template>
  <m-tooltip
    v-if="title"
    :content="title"
    :placement="posTooltip"
    :effect="isEffect"
    :hide-after="0"
  >
    <template #child>
      <button :type="type" :class="className" @click.stop="$emit('click')">
        <div :class="iconButton"></div>
      </button>
    </template>
  </m-tooltip>
  <button v-else :type="type" :class="className" @focus="handleFocus">
    <div :class="iconButton"></div>
    <div v-if="content" class="text__btn">
      {{ content }}
    </div>
    <div v-if="iconSeccond" :class="iconSeccond" class="iconSeccond"></div>
  </button>
</template>

<script>
import MTooltip from "./MTooltip.vue";

export default {
  name: "MButton",
  data() {
    return {
      isFocus: false,
    };
  },
  props: [
    "className",
    "iconButton",
    "content",
    "title",
    "type",
    "posTooltip",
    "isEffect",
    "iconSeccond",
  ],
  components: {
    "m-tooltip": MTooltip,
  },
  methods: {
    handleFocus() {
      this.isFocus = true;
    },
  },
};
</script>

<style>
@import "@/css/base/button.css";
.text__btn {
  display: none;
}
</style>
