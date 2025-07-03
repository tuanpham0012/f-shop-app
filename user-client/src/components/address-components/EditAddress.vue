<template>
    <!-- Overlay -->
    <div class="modal-overlay" @click="closeModal">
        <!-- Address List Modal -->
        <div v-if="currentView === 'list'" class="modal1" @click.stop>
            <div class="modal-header">
                <h2>Địa Chỉ Của Tôi</h2>
                <button @click="closeModal" class="close-btn">×</button>
            </div>

            <div class="modal-body">
                <div class="address-list">
                    <div v-for="address in deliveryAddresses" :key="address.id" class="address-item" :class="{ 'default-address': address.default }">
                        <div class="address-icon">
                            <div class="location-dot"></div>
                        </div>
                        <div class="address-content">
                            <div class="address-name">{{ address.fullName }}</div>
                            <div class="address-phone">{{ address.phone }}</div>
                            <div class="address-detail">{{ address.detailAddress }}</div>
                            <span v-if="address.default" class="default-badge">Mặc định</span>
                        </div>
                        <button class="update-btn">Cập nhật</button>
                    </div>
                </div>

                <button @click="showAddAddressForm" class="add-address-btn">
                    <span class="plus-icon">+</span>
                    Thêm Địa Chỉ Mới
                </button>
            </div>

            <div class="modal-footer">
                <button @click="closeModal" class="cancel-btn">Hủy</button>
                <button class="confirm-btn">Xác nhận</button>
            </div>
        </div>

        <!-- Add Address Form Modal -->
        <div v-if="currentView === 'add'" class="modal1" @click.stop>
            <div class="modal-header">
                <h2>Địa chỉ mới</h2>
                <button @click="closeModal" class="close-btn">×</button>
            </div>

            <div class="modal-body">
                <div class="form-row">
                    <div class="form-group">
                        <input v-model="newAddress.fullName" type="text" placeholder="Họ và tên" class="form-input" required />
                    </div>
                    <div class="form-group">
                        <input v-model="newAddress.phone" type="tel" placeholder="Số điện thoại" class="form-input" required />
                    </div>
                </div>
                <!-- <div class="form-row">
                    <div class="form-group">
                        <select v-model="newAddress.provinceId" class="form-select" required>
                            <option :value="null">Tỉnh/Thành phố</option>
                            <option v-for="(item, index) in provinces" :key="index" :value="item.id">{{ item.shortName }}</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <select v-model="newAddress.wardId" class="form-select" required>
                            <option :value="null">Phường/Xã</option>
                            <option v-for="(item, index) in wards" :key="index" :value="item.id">{{ item.name }}</option>
                        </select>
                    </div>
                </div> -->
                <div class="form-row">
                    <div class="form-group">
                        <SelectSearch v-model="newAddress.provinceId" :listData="provinces" placeholder="Tỉnh/Thành phố" display="shortName" ref="provinceSelect" required />
                    </div>
                    <div class="form-group">
                        <SelectSearch v-model="newAddress.wardId" :listData="wards" placeholder="Phường/Xã" display="shortName" ref="wardSelect" required />
                    </div>
                </div>

                <div class="form-group">
                    <textarea v-model="newAddress.address" placeholder="Địa chỉ cụ thể" class="form-textarea" rows="3" required></textarea>
                </div>

                <button type="button" class="add-location-btn">
                    <span class="plus-icon">+</span>
                    Thêm vị trí
                </button>
            </div>
            <div class="form-group px-5">
                <div class="form-check">
                    <input class="form-check-input" type="checkbox" v-model="newAddress.default" id="checkChecked" />
                    <label class="form-check-label" for="checkChecked"> Đặt làm địa chỉ mặc định </label>
                </div>
            </div>
            <div class="modal-footer">
                <button @click="backToList" class="cancel-btn">Trở Lại</button>
                <button @click="save" class="confirm-btn">Hoàn thành</button>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { ref, reactive, computed, onBeforeMount, watch } from "vue";
import { useCommonStore } from "@/stores/common";
import { useUserStore } from "@/stores/user";
import SelectSearch from "../input-form/SelectSearch.vue";
import { successMessage } from "@/helpers/toast";

const commonStore = useCommonStore();
const userStore = useUserStore();

interface Address {
    id: number | null;
    fullName: string;
    phone: string;
    address: string;
    detailAddress: string;
    default: boolean;
    provinceId: number | null;
    wardId: number | null;
}

const showAddressModal = ref(false);
const currentView = ref<"list" | "add">("list");

const provinces = computed<any>(() => commonStore.provinces.data);
const wards = computed<any>(() => commonStore.wards.data);

const deliveryAddresses = computed(() => userStore.deliveryAddresses.data);

const wardSelect = ref<any>(null);

const newAddress = reactive<Address>({
    id: null,
    fullName: "",
    phone: "",
    address: "",
    provinceId: null,
    wardId: null,
    detailAddress: "",
    default: deliveryAddresses.value.length > 0 ? false : true,
});

const showAddAddressForm = () => {
    currentView.value = "add";
    commonStore.getProvinces();
};

const backToList = () => {
    currentView.value = "list";
    userStore.getListDeliveryAddresses({ page: 1, limit: 10 });
};

const closeModal = () => {
    showAddressModal.value = false;
    currentView.value = "list";
    // Reset form
    Object.assign(newAddress, {
        fullName: "",
        phone: "",
        location: "",
        detailAddress: "",
        type: "home",
    });
};

const save = async () => {
    // Handle save logic here
    await userStore
        .createDeliveryAddresses(newAddress)
        .then(() => {
            console.log("Address saved successfully");
            successMessage("Địa chỉ đã được lưu thành công!");
        })
        .catch((error) => {
            console.error("Error saving address:", error);
        });
    // Reset form and go back to list view
    backToList();
};

watch(
    () => newAddress.provinceId,
    (newProvinceId) => {
        if (newProvinceId) {
            commonStore.getWards(newProvinceId);
            setTimeout(() => {
                console.log(wardSelect.value);
                wardSelect.value.toggleShow(true);
            }, 100);
        } else {
            commonStore.wards.data = [];
        }
    }
);

onBeforeMount(() => {
    commonStore.getProvinces();
    userStore.getListDeliveryAddresses({ page: 1, limit: 10 });
});
</script>

<style lang="scss" scoped>
.open-modal-btn:hover {
    background-color: #e55a2b;
}

.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 1000;
}

.modal1 {
    background: white;
    border-radius: 12px;
    width: 100%;
    max-width: 600px;
    max-height: 90vh;
    overflow: hidden;
    display: flex;
    flex-direction: column;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.3);
}

.modal-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 20px;
    border-bottom: 1px solid #e0e0e0;
}

.modal-header h2 {
    margin: 0;
    font-size: 18px;
    font-weight: 600;
    color: #333;
}

.close-btn {
    background: none;
    border: none;
    font-size: 24px;
    cursor: pointer;
    color: #666;
    width: 30px;
    height: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.close-btn:hover {
    color: #333;
}

.modal-body {
    flex: 1;
    padding: 20px;
    overflow-y: auto;
}

.address-list {
    margin-bottom: 20px;
    .default-address{
        background-color: #f0f0f0;
    }
}

.address-item {
    display: flex;
    align-items: flex-start;
    padding: 16px 12px;
    border-radius: 8px;
    border-bottom: 1px solid #f0f0f0;
}

.address-item:last-child {
    border-bottom: none;
}

.address-icon {
    margin-right: 12px;
    margin-top: 4px;
}

.location-dot {
    width: 12px;
    height: 12px;
    background-color: var(--bs-primary);
    border-radius: 50%;
}

.address-content {
    flex: 1;
}

.address-name {
    font-weight: 600;
    color: #333;
    margin-bottom: 4px;
}

.address-phone {
    color: #666;
    font-size: 14px;
    margin-bottom: 4px;
}

.address-detail {
    color: #666;
    font-size: 14px;
    line-height: 1.4;
    white-space: pre-line;
}

.default-badge {
    display: inline-block;
    background-color: var(--bs-primary);
    color: white;
    font-size: 12px;
    padding: 2px 8px;
    border-radius: 4px;
    margin-top: 8px;
}

.update-btn {
    background: none;
    border: 1px solid #4a90e2;
    color: #4a90e2;
    padding: 8px 16px;
    border-radius: 4px;
    font-size: 14px;
    cursor: pointer;
    transition: all 0.2s;
}

.update-btn:hover {
    background-color: #4a90e2;
    color: white;
}

.add-address-btn {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100%;
    padding: 16px;
    border: 2px dashed #ccc;
    background: none;
    color: #666;
    border-radius: 8px;
    cursor: pointer;
    transition: all 0.2s;
}

.add-address-btn:hover {
    border-color: var(--bs-primary);
    color: var(--bs-primary);
}

.plus-icon {
    margin-right: 8px;
    font-size: 18px;
    font-weight: bold;
}

.form-row {
    display: flex;
    gap: 16px;
    margin-bottom: 16px;
}

.form-group {
    flex: 1;
    margin-bottom: 16px;
}

.form-input,
.form-select,
.form-textarea {
    width: 100%;
    padding: 12px;
    border: 1px solid #ddd;
    border-radius: 6px;
    font-size: 14px;
    transition: border-color 0.2s;
    box-sizing: border-box;
}

.form-input:focus,
.form-select:focus,
.form-textarea:focus {
    outline: none;
    border-color: #4a90e2;
}

.form-textarea {
    resize: vertical;
    min-height: 80px;
}

.add-location-btn {
    display: flex;
    align-items: center;
    background: none;
    border: none;
    color: #4a90e2;
    cursor: pointer;
    margin-bottom: 20px;
    font-size: 14px;
}

.add-location-btn:hover {
    text-decoration: underline;
}

.address-type {
    margin-bottom: 20px;
}

.address-type-label {
    display: block;
    margin-bottom: 12px;
    font-weight: 500;
    color: #333;
}

.address-type-options {
    display: flex;
    gap: 20px;
}

.address-type-option {
    display: flex;
    align-items: center;
    cursor: pointer;
}

.address-type-option input[type="radio"] {
    margin-right: 8px;
}

.radio-label {
    padding: 8px 16px;
    border: 1px solid #ddd;
    border-radius: 20px;
    font-size: 14px;
    transition: all 0.2s;
}

.address-type-option input[type="radio"]:checked + .radio-label {
    background-color: #4a90e2;
    color: white;
    border-color: #4a90e2;
}

.modal-footer {
    display: flex;
    justify-content: flex-end;
    gap: 12px;
    padding: 20px;
    border-top: 1px solid #e0e0e0;
}

.cancel-btn,
.confirm-btn {
    padding: 10px 24px;
    border-radius: 6px;
    font-size: 14px;
    cursor: pointer;
    transition: all 0.2s;
}

.cancel-btn {
    background: none;
    border: 1px solid #ddd;
    color: #666;
}

.cancel-btn:hover {
    border-color: #999;
    color: #333;
}

.confirm-btn {
    background-color: var(--bs-primary);
    border: none;
    color: white;
}

.confirm-btn:hover {
    background-color: #e55a2b;
}

/* Hide default radio buttons */
.address-type-option input[type="radio"] {
    display: none;
}
</style>
