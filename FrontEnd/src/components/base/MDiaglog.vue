<template>
  <div class="dialog__container">
    <slot name="content"></slot>
  </div>
</template>

<script lang="js">
export default {
  name: "MDialog",
  props: ["content","isShow","MForm"],
  data() {
    return {

    };
  },
  methods:{

  }
};
</script>

<style></style>
