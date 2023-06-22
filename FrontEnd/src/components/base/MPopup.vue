<template>
  <div class="popup__container">
    <div class="popup__icon" :class="icon"></div>
    <div class="popup__content">
      <div :class="['popup__text', { 'mwidth--300': dataPopup.length > 1 }]">
        <template v-if="dataPopup.length > 1">
          <div>
            <span style="font-weight: bold">
              {{
                dataPopup.length < 10
                  ? `0${dataPopup.length}`
                  : dataPopup.length
              }}</span
            >
            {{ this.$_MISAResources.popup.delete_more }}
          </div>
        </template>
        <template v-else-if="dataPopup.length === 1">
          <div class="property">
            {{ this.$_MISAResources.popup.delete_only }}
            <b>&lt;&lt;{{ dataPopup[0].AssetCode }}</b>
            -
            <b>
              {{ dataPopup[0].AssetName }}
              &gt;&gt; ?
            </b>
          </div>
        </template>
      </div>
      <div class="popup__btn">
        <m-button
          content="Hủy bỏ"
          className="btn--sm-pop btn__cancel"
          @click="handleBtnCancel"
        ></m-button>
        <m-button
          content="Xóa"
          className="btn--sm-pop btn__main"
          @click="handleDelete(dataPopup)"
        ></m-button>
      </div>
    </div>
  </div>
</template>

<script>
import MButton from "./MButton.vue";
import { mapActions, mapState } from "vuex";

export default {
  name: "MPopup",
  props: ["dataPopup", "icon", "isShow"],
  components: {
    "m-button": MButton,
  },
  mounted() {
    console.log(this.dataPopup);
  },

  computed: {
    ...mapState("property", ["isLoading", "propertyList", "listSelected"]),
  },
  updated() {
    console.log(this.dataPopup);
    this.isShowPopup = this.isShow;
  },
  data() {
    return {
      isShowPopup: this.isShow,
    };
  },
  methods: {
    ...mapActions("property", ["delete", "setListSelected", "deleteMulti"]),
    /**
     * function emit close form when click cancel
     * Author: HMDUC(27/05/2023)
     */
    handleBtnCancel() {
      this.$emit("cancel");
    },
    /**
     * function confirm delete property selected
     * Author: HMDUC(27/05/2023)
     */
    async handleDelete(data) {
      let lenghtList = data.length;
      switch (lenghtList) {
        case 1:
          try {
            const res = await this.delete(data[0].AssetId);
            const deleteIndex = this.listSelected.findIndex(
              (item) => item.AssetId == data[0].AssetId
            );
            this.listSelected.splice(deleteIndex, 1);
            if (res === 204) {
              this.$emit("showToast", "success");
            } else if (res === 404) {
              this.$emit("showToast", "err");
            }
          } catch (err) {
            this.$emit("showToast", "err");
          }
          break;
        default:
          try {
            let listId = [];
            data.forEach((item) => {
              listId.push(item.AssetId);
            });
            const res = await this.deleteMulti(listId);
            const deleteIndices = [];

            // remove asset in store
            this.listSelected.forEach((item, index) => {
              if (listId.includes(item.AssetId)) {
                deleteIndices.push(index);
              }
            });
            deleteIndices.reverse().forEach((index) => {
              this.listSelected.splice(index, 1);
            });
            if (res === 200) {
              this.$emit("showToast", "success");
            } else if (res === 404) {
              this.$emit("showToast", "err");
            }
          } catch (err) {
            this.$emit("showToast", "err");
          }
          break;
      }
    },
  },
};
</script>

<style scoped>
@import url(@/css/base/popup.css);
</style>
