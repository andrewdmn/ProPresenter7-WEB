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
    getThumbnailUrl,
    triggerNextSlide,
    triggerPrevSlide,
    triggerSlide
} from './services/ProPresenterService';

import { Presentation, ActiveSlideIndex } from './core/index';

function App() {
    const [activeSlideIndex, setActiveSlideIndex] = useState<ActiveSlideIndex | null>(null);
    const [presentationDetails, setPresentationDetails] = useState<Presentation | null>(null);

    useEffect(() => {
        Init();
    }, []);

    async function Init(): Promise<void> {
        const presentationDetails = await getPresentationDetails();
        setPresentationDetails(presentationDetails);

        const activeSlideIndex = await getActiveSlideIndex();
        setActiveSlideIndex(activeSlideIndex);
    }

    function slideImages(): any[] | null {
        if (presentationDetails == null) {
            return null;
        }

        const slides = [];

        for (let i = 0; i < presentationDetails.slideCount; i++) {
            slides.push(<Image
                src={getThumbnailUrl(i)}
                className={i === activeSlideIndex?.slideIndex ? 'slide active-slide' : 'slide'}
                key={i}
                width='300'
                onClick={() => {
                    setActiveSlideIndex(null);
                    onTriggerSlide(i);
                }} />);
        }

        return slides;
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
                center={presentationDetails?.name}>
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
