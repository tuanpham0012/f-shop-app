<template>
    <Modal title="Tải lên tập tin" modal-size="modal-lg">
        <template #body>
            <div class="d-flex gap-3 justify-between">
                <Cropper
                    class="w-[350px] h-[350px]"
                    :src="option.src"
                    :auto-zoom="true"
                    :stencil-size="{
                        width: 300,
                        height: 300,
                    }"
                    :debounce="false"
                    image-restriction="stencil"
                    :stencil-props="{
                        aspectRatio: 1,
                    }"
                    :stencil-component="CircleStencil"
                    ref="cropper"
                    @change="onChange"
                />
                <preview
                    class="img-radius"
                    :width="350"
                    :height="350"
                    :image="option.image"
                    :coordinates="option.coordinates"
                />
            </div>
            <input type="file" @change="loadImage"  accept="image/png,image/jpeg"/>
        </template>
        <template #footer>
            <button class="btn btn-sm btn-success" @click="saveFile()">
                Lưu lại
            </button>
            <button
                class="btn btn-sm btn-secondary"
                @click="
                    () => {
                        emits('close');
                    }
                "
            >
                Đóng
            </button>
        </template>
    </Modal>
</template>
<script setup>
import { ref, reactive } from "vue";
import Modal from "./BaseModal.vue";
import { CircleStencil, Preview } from "vue-advanced-cropper";
import axios from "axios";

const option = reactive({
    src: null,
    image: null,
    name: null,
    size: "",
    outputType: "jpg",
    autoCropWidth: 350,
    autoCropHeight: 350,
    coordinates: null,
});

const emits = defineEmits(['uploadSuccess', 'close'])
const cropper = ref(null);

const onChange = ({ coordinates, image, canvas }) => {
    option.coordinates = coordinates;
    // You able to do different manipulations at a canvas
    // but there we just get a cropped image, that can be used
    // as src for <img/> to preview result
    option.image = image;
};

const loadImage = (event) => {
    // Reference to the DOM input element
    const { files } = event.target;
    // Ensure that you have a file before attempting to read it
    if (files && files[0]) {
        // 1. Revoke the object URL, to allow the garbage collector to destroy the uploaded before file
        if (option.src) {
            URL.revokeObjectURL(option.src);
        }
        option.name = files[0].name;
        // 2. Create the blob link to the file to optimize performance:
        const blob = URL.createObjectURL(files[0]);

        // Create a new FileReader to read this image binary data
        const reader = new FileReader();
        // Define a callback function to run, when FileReader finishes its job
        reader.onload = (e) => {
            // Note: arrow function used here, so that "this.image" refers to the image of Vue component
            option.src = blob;
        };
        // Start the reader job - read file as a data url (base64 format)
        reader.readAsArrayBuffer(files[0]);
    }
};

const saveFile = () => {
    const form = new FormData();
    const { canvas } = cropper.value.getResult();
    canvas.toBlob((blob) => {
        console.log(blob.type);
        form.append("file", new File([blob], option.name, {type:blob.type, lastModified:new Date().getTime()}));
        // form.append("name", blob.type);
        axios({
            url: "/api/file/upload",
            method: "POST",
            headers: {},
            data: form,
        })
            .then((res) => {
                console.log(res);
                emits('uploadSuccess', { file_id: res.data.file_id, url: res.data.url, name: option.name});
            })
            .catch((err) => {
                console.log(err);
            });
    }, "image/jpeg");
};
</script>
<style lang=""></style>
