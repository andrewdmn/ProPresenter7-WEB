import './App.css';
import { Image } from 'primereact/image';
import { Button } from 'primereact/button';
import { Toolbar } from 'primereact/toolbar';
import { ScrollPanel } from 'primereact/scrollpanel';
import { Panel } from 'primereact/panel';

import { useState, useEffect } from 'react';

import {
    getActiveSlideIndex,
    getPresentationDetails,
    getThumbnail,
    triggerNextSlide,
    triggerPrevSlide,
    triggerSlide
} from './services/ProPresenterService';

import { Presentation, ActiveSlideIndex } from './core/index';

function App() {
    const [activeSlideIndex, setActiveSlideIndex] = useState<ActiveSlideIndex | null>(null);
    const [presentationDetails, setPresentationDetails] = useState<Presentation | null>(null);
    const [slideImageUrls, setSlideImageUrls] = useState<string[]>([]);

    useEffect(() => {
        Init();
    }, []);

    async function Init(): Promise<void> {
        const presentationDetails = await getPresentationDetails();
        setPresentationDetails(presentationDetails);

        const activeSlideIndex = await getActiveSlideIndex();
        setActiveSlideIndex(activeSlideIndex);

        const slideImageUrls: string[] = [];
        
        for (let i = 0; i < presentationDetails?.slideCount; i++) {
            const blob = await getThumbnail(i);
            const imageUrl = URL.createObjectURL(blob);
            slideImageUrls.push(imageUrl);
        }

        setSlideImageUrls(slideImageUrls);
    }

    function slideImages(): any[] | null {
        if (presentationDetails == null) {
            return null;
        }

        const slides: any[] = [];

        slideImageUrls.forEach((slideImageUrl, index) => {
            slides.push(
                <Image
                    src={slideImageUrl}
                    className={index === activeSlideIndex?.slideIndex ? 'slide active-slide' : 'slide'}
                    key={index}
                width='300'
                onClick={() => {
                    setActiveSlideIndex(null);
                    onTriggerSlide(index);
                }} />);
        });

        return slides;
        }

    function refreshButton(): any {
        return <Button
            icon="pi pi-refresh"
            tooltip="Refresh"
            onClick={Init} />
    }

    function onTriggerSlide(slideIndex: number): void {
        triggerSlide(slideIndex).then(() => {
            setTimeout(() => {
                getActiveSlideIndex().then(slideIndex => {
                    setActiveSlideIndex(slideIndex);
                });
            }, 500);
        });
    }

    function onTriggerNextSlide() {
        triggerNextSlide().then(() => {
            setTimeout(() => {
                getActiveSlideIndex().then(slideIndex => {
                    setActiveSlideIndex(slideIndex);
                });
            }, 500);
        });
    }

    function onTriggerPrevSlide() {
        triggerPrevSlide().then(() => {
            setTimeout(() => {
                getActiveSlideIndex().then(slideIndex => {
                    setActiveSlideIndex(slideIndex);
                });
            }, 500);
        });
    }

    return (
        <div className="App">
            <Toolbar
                center={presentationDetails?.name}
                end={refreshButton()}>
            </Toolbar>
            <div className='slide-container'>
                <ScrollPanel style={{ height: "calc(100vh - 260px)", width: "100%" }} className="scroll">
                    {slideImages()}
                </ScrollPanel>
            </div>

            <Panel>
                <Button
                    icon="pi pi-caret-left"
                    size='large'
                    tooltip='Previous slide'
                    tooltipOptions={{ position: 'top' }}
                    onClick={onTriggerPrevSlide}
                />
                <Button
                    icon="pi pi-caret-right"
                    size='large'
                    tooltip='Next slide'
                    tooltipOptions={{ position: 'top' }}
                    onClick={onTriggerNextSlide}
                />
            </Panel>
        </div>
    );
}

export default App;
