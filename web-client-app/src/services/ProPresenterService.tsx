import { Presentation, Slide } from '../core';

const apiUrl = import.meta.env.VITE_API_URL;

export async function getPresentationDetails(): Promise<Presentation> {
    const response = await fetch(`${apiUrl}/api/Presentation`);
    const presentation = await response.json();

    return presentation;
}

export function getThumbnailUrl(slideIndex: any): string {
    return `${apiUrl}/api/Presentation/Slide/${slideIndex}`;
}

export async function getActiveSlideIndex(): Promise<Slide> {
    // TODO: Return index of an active slide.
    throw new Error("Not implemented");
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