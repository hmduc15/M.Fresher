<template>
  <div v-if="isShow" class="toast__container">
    <div class="toast__icon">
      <template v-if="type === 'success' || type === 'success__add'">
        <div class="icon__circle">
          <div :class="types[type].icon"></div>
        </div>
      </template>
      <template v-else>
        <div :class="types[type].icon"></div>
      </template>
    </div>
    <div class="toast__content">
      {{ content ? content : types[type].content }}
    </div>
    <button class="btn__close--toast">
      <div class="icon__close-sm"></div>
    </button>
  </div>
</template>

<script>
export default {
  name: "MToast",
  props: ["content", "icon", "isShow", "type", "labelField"],
  data() {
    return {
      types: {
        success: {
          icon: "icon__check",
          content: this.$_MISAResources.toast__content.DeleteSuccess,
        },
        success__add: {
          icon: "icon__check",
          content: this.$_MISAResources.toast__content.InsertSuccess,
        },
        err: {
          icon: "icon__delete--red",
          content: this.$_MISAResources.toast__content.DeleteError,
        },
        err__add: {
          icon: "icon__delete--red",
          content: this.$_MISAResources.toast__content.InsertError,
        },
        notice: {
          icon: "icon__notice",
        },
        warning: {
          icon: "icon__warning",
          content: this.$_MISAResources.toast__content.NotAsset,
        },
      },
    };
  },
};
</script>

<style>
@import url(@/css/base/toast.css);
</style>
