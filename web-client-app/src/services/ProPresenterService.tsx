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

export async function triggerSlide(presentationUuid: any, slideIndex: any) {
    // TODO: Triger a slide
    throw new Error("Not implemented");
}

export async function triggerNextSlide() {
    // TODO: Trigger next slide.
    throw new Error("Not implemented");
}

export async function triggerPrevSlide() {
    // TODO: Trigger previous slide.
    throw new Error("Not implemented");
}