<template>
  <label class="label__input">{{ label }}</label>
  <VueDatePicker v-model="dateVal" :format="dateFormat" auto-apply>
  </VueDatePicker>
</template>

<script>
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";

export default {
  name: "MDatepicker",
  props: ["label"],
  components: {
    VueDatePicker,
  },
  data() {
    return {
      dateVal: new Date(),
      /**
       * format date
       * return 28/05/2023
       */
      dateFormat: "dd/MM/yyyy",
    };
  },
};
</script>

<style>
@import url(@/css/base/input.css);
</style>
