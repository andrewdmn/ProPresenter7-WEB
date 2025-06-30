import { Presentation, ActiveSlideIndex } from '../core';

const apiUrl = import.meta.env.VITE_API_URL;

export async function getPresentationDetails(): Promise<Presentation> {
    const response = await fetch(`${apiUrl}/api/Presentation`);
    const presentation = await response.json();

    return presentation;
}

export async function getThumbnail(presentationUuid: string, slideIndex: number): Promise<Blob> {
    const response = await fetch(`${apiUrl}/api/Presentation/${presentationUuid}/${slideIndex}/Image`)
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

export async function triggerSlide(presentationUuid: string, slideIndex: any) {
    await fetch(`${apiUrl}/api/Presentation/${presentationUuid}/${slideIndex}/Trigger`);
}

export async function triggerNextSlide(presentationUuid: string) {
    await fetch(`${apiUrl}/api/Presentation/${presentationUuid}/Next/Trigger`);
}

export async function triggerPrevSlide(presentationUuid: string) {
    await fetch(`${apiUrl}/api/Presentation/${presentationUuid}/Previous/Trigger`);
}