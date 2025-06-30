import { Presentation, ActiveSlideIndex } from '../core';

const apiUrl = import.meta.env.VITE_API_URL;

export async function getPresentationDetails(): Promise<Presentation> {
    const response = await fetch(`${apiUrl}/api/Presentation`);
    const presentation = await response.json();

    return presentation;
}

export async function getThumbnail(slideIndex: number): Promise<Blob> {
    const response = await fetch(`${apiUrl}/api/Slide/${slideIndex}/Image`)
    const image = await response.blob();

    return image;
}

export async function getActiveSlideIndex(): Promise<ActiveSlideIndex | null> {
    const response = await fetch(`${apiUrl}/api/Slide/Active/Index`);

    if (response.status === 204) {
        return null;
    }

    const activeSlideIndex = await response?.json();
    return activeSlideIndex;
}

export async function triggerSlide(slideIndex: any) {
    await fetch(`${apiUrl}/api/Slide/${slideIndex}/Trigger`);
}

export async function triggerNextSlide() {
    await fetch(`${apiUrl}/api/Slide/Next/Trigger`);
}

export async function triggerPrevSlide() {
    await fetch(`${apiUrl}/api/Slide/Previous/Trigger`);
}