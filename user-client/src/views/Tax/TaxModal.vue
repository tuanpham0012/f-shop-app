<template>
    <modal
        :title="!id ? 'Thêm mới thuế sản phẩm' : 'Cập nhật thuế sản phẩm'"
        modal-size="modal-lg"
        @close="
            () => {
                emits('close');
            }
        "
    >
        <template #body>
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-6 mb-3">
                        <div class="col-sm-12">
                            <label for="name" class="form-label required"
                                >Tên thuế</label
                            >
                        </div>
                        <div class="input-group">
                            <input
                                type="text"
                                class="form-control"
                                id="name"
                                v-model="tax.name"
                            />
                        </div>
                        <Feedback :errors="errors?.Name" />
                    </div>

                    <div class="col-sm-6 mb-3">
                        <div class="col-sm-12">
                            <label for="email" class="form-label required"
                                >Giá trị</label
                            >
                        </div>
                        <div class="input-group">
                            <input
                                type="email"
                                class="form-control"
                                id="email"
                                v-model="tax.value"
                            />
                        </div>
                        <Feedback :errors="errors?.value" />
                    </div>
                    <div class="col-sm-6 mb-3">
                        <input
                            class="form-check-input me-2"
                            type="checkbox"
                            id="not_use"
                            v-model="tax.notUse"
                        />
                        <label for="not_use" class=""
                            >Ngưng sử dụng</label
                        >
                        <Feedback :errors="errors?.Email" />
                    </div>
                </div>
            </div>
        </template>
        <template #footer>
            <button class="btn btn-success" @click="save()">Lưu lại</button>
            <button
                class="btn btn-secondary"
                @click="
                    () => {
                        emits('close');
                    }
                "
            >
                Đóng
            </button>
        </template>
    </modal>
</template>
<script lang="ts" setup>
import { ref, reactive, computed, onBeforeMount } from "vue";
import { useTaxStore } from "@/stores/tax";
import { successMessage } from "@/helpers/toast";
const props = defineProps({
    id: {
        type: [Number, String as () => string | null],
        required: false,
        default: null,
    },
});

const emits = defineEmits(["close"]);

const taxStore = useTaxStore();

const newTax = reactive({
    id: null,
    name: "",
    value: "",
    notUse: false,
});

const tax = computed(() =>
    props.id && taxStore.$state.entry ? taxStore.$state.entry : newTax
);

const errors = ref<any>(null);

const save = async () => {
    if (tax.value.id == null) {
        await taxStore
            .create(tax.value)
            .then((res) => {
                successMessage(res.data?.message ?? "Thêm mới thành công!");
                emits("close", res.data.data);
            })
            .catch((err) => {
                errors.value = err.response.data.errors;
            });
    } else {
        await taxStore
            .update(props.id, tax.value)
            .then((res) => {
                successMessage(res.data?.message ?? "Cập nhật thành công!");
                emits("close", true);
            })
            .catch((err) => {
                console.log(err);
                errors.value = err.response.data.errors;
            });
    }
};
onBeforeMount(() => {
    if (props.id) {
        taxStore.show(props.id);
    }
});
</script>
<style lang=""></style>
