<template>
  <div class="row department--delivery">
    <div class="stt--increament">
      <m-input
        type="text"
        className="input__text input--sm"
        :required="false"
        :requiredNot="true"
        v-model="index"
        id="stt"
      ></m-input>
    </div>
    <div class="col-3">
      <input
        type="text"
        className="input__text input--md"
        :required="false"
        :requiredNot="true"
        name="fullName"
        id="fullName"
        :placeHolder="this.$_MISAResources.placeholder.FullName"
        v-model="delivery.FullName"
      />
    </div>
    <div class="col-3">
      <m-input
        type="text"
        className="input__text input--md"
        :required="false"
        :requiredNot="true"
        name="representation"
        id="representation"
        v-model="delivery.Representation"
        :placeHolder="this.$_MISAResources.placeholder.Representation"
      ></m-input>
    </div>
    <div class="col-3">
      <m-input
        type="text"
        className="input__text input--md"
        :required="false"
        :requiredNot="true"
        name="positionName"
        id="positionName"
        :placeHolder="this.$_MISAResources.placeholder.PositionName"
        v-model="delivery.PositionName"
      ></m-input>
    </div>

    <div class="col-2 action">
      <m-tooltip
        placement="top"
        :content="this.$_MISAResources.tooltip__btn.up"
        v-if="isShow"
      >
        <template #child>
          <div
            class="icon__detail-up btn__delivery"
            @click="moveUp(index, 'up')"
          ></div>
        </template>
      </m-tooltip>
      <m-tooltip
        placement="top"
        :content="this.$_MISAResources.tooltip__btn.down"
        v-if="isShow"
      >
        <template #child>
          <div
            class="icon__detail-down btn__delivery"
            @click="moveUp(index, 'down')"
          ></div>
        </template>
      </m-tooltip>
      <m-tooltip
        placement="top"
        :content="this.$_MISAResources.tooltip__btn.add"
      >
        <template #child>
          <div
            class="icon__detail-add btn__delivery"
            @click="handleAdd(index)"
          ></div>
        </template>
      </m-tooltip>
      <m-tooltip
        :content="this.$_MISAResources.tooltip__btn.delete"
        placement="top"
      >
        <template #child>
          <div
            v-if="index > 1"
            class="icon__delete--red btn__delivery"
            @click="handleRemove(index)"
          ></div>
        </template>
      </m-tooltip>
    </div>
  </div>
</template>

<script>
import MInput from "@/components/base/MInput";
import MTooltip from "@/components/base/MTooltip";

export default {
  name: "MDepartmentDynamic",
  components: {
    "m-input": MInput,
    "m-tooltip": MTooltip,
  },

  props: ["componentData", "order", "isOnlyComponent", "isShowAction"],
  data() {
    return {
      delivery: {
        stt: "",
        FullName: "",
        Representation: "",
        PositionName: "",
      },
      index: null,
      isShow: true,
      isOnly: true,
    };
  },
  created() {
    this.delivery = this.componentData;
    this.index = this.order;
  },

  updated() {},

  watch: {
    componentData(newValue) {
      this.delivery = newValue;
    },
    isShowAction(newValue) {
      this.isShow = newValue;
    },
  },

  methods: {
    /**
     * Function get value dynamic component
     * Author: HMDUC (29/07/2023)
     */
    getValue() {
      return this.delivery;
    },

    /**
     * Function add dynamic component
     * Author: HMDUC (29/07/2023)
     */
    handleAdd(index) {
      this.isShow = true;
      this.$emit("show", index);
      this.$emit("add");
    },

    /**
     * Function remove dynamic component
     * Author: HMDUC (29/07/2023)
     */
    handleRemove(index) {
      this.$emit("remove", index);
    },

    moveUp(index, type) {
      this.$emit("moveUp", index, type);
    },

    // handleInput(value) {
    //   return value.replace(/[^a-zA-Z0-9]/g, "");
    // },
  },
};
</script>

<style>
.department--delivery {
  gap: 0 15px;
  margin-bottom: 10px;
  align-items: center;
  user-select: none;
}

.form {
  display: flex;
}

.department--delivery > .col-3 {
  padding: 0;
}

.action {
  padding: 0;
  display: flex;
}
.btn__delivery {
  margin: 0 10px;
  cursor: pointer;
}
</style>
