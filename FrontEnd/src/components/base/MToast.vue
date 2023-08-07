<template>
  <div v-if="isShow" class="toast__container">
    <div class="toast__icon">
      <template
        v-if="
          type === 'success' ||
          type === 'success__add' ||
          type === 'success__update' ||
          type === 'success__receipt' ||
          type === 'success__add_receipt' ||
          type === 'success__update_receipt'
        "
      >
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
        success__update: {
          icon: "icon__check",
          content: this.$_MISAResources.toast__content.UpdateSuccess,
        },
        success__update_receipt: {
          icon: "icon__check",
          content: this.$_MISAResources.toast__content.UpdateReceiptSuccess,
        },
        success: {
          icon: "icon__check",
          content: this.$_MISAResources.toast__content.DeleteSuccess,
        },
        success__receipt: {
          icon: "icon__check",
          content: this.$_MISAResources.toast__content.DeleteReceiptSuccess,
        },
        err__receipt: {
          icon: "icon__delete--red",
          content: this.$_MISAResources.toast__content.DeleteReceiptError,
        },
        success__add_receipt: {
          icon: "icon__check",
          content: this.$_MISAResources.toast__content.InsertReceiptSuccess,
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
        err__update: {
          icon: "icon__warning",
          content: this.$_MISAResources.toast__content.UpdateError,
        },

        notice: {
          icon: "icon__notice--b",
        },
        warning: {
          icon: "icon__warning--sm",
          content: this.$_MISAResources.toast__content.NotAsset,
        },
        no__edit: {
          icon: "icon__notice--b",
          content: this.$_MISAResources.toast__content.NoEdit,
        },
        no__edit_receipt: {
          icon: "icon__notice--b",
          content: this.$_MISAResources.toast__content.NoEditReceipt,
        },
      },
    };
  },
};
</script>

<style>
@import url(@/css/base/toast.css);
</style>
