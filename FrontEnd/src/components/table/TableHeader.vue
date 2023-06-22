<template>
  <thead class="table__head">
    <tr>
      <th
        v-for="(column, index) in columns"
        :key="index"
        :class="[
          `${column.posLeft ? 'align--left' : 'align--right'}`,
          `${column.action ? 'btn__row align--center' : ''}`,
        ]"
        :style="[`${column.width ? `width: ${column.width}px` : ''}`]"
      >
        <m-input
          v-if="column.checkbox && !column.title"
          type="checkbox"
          className="table__checkbox"
          name="ckbox"
          id="ckbox"
          :isDisabled="true"
        ></m-input>
        <template v-else-if="column.title && !column.tooltip">
          {{ column.title }}
        </template>
        <template v-else-if="column.title && column.tooltip">
          <m-tooltip :content="column.tooltip" placement="top" effect="light">
            <template #child> {{ column.title }}</template>
          </m-tooltip>
        </template>
      </th>
    </tr>
  </thead>
</template>

<script>
import MInput from "../base/MInput.vue";
import MTooltip from "../base/MTooltip.vue";

export default {
  name: "TableHeader",
  props: ["columns"],
  components: {
    "m-input": MInput,
    "m-tooltip": MTooltip,
  },
  data() {
    return {};
  },
};
</script>

<style>
@import "@/css/base/table.css";
</style>
