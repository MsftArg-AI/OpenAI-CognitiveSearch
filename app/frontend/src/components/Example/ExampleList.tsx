import { Example } from "./Example";

import styles from "./Example.module.css";

export type ExampleModel = {
    text: string;
    value: string;
};

const EXAMPLES: ExampleModel[] = [
    {
        text: "Que lugares emblematicos puedo conocer en la Ciudad de Buenos Aires?",
        value: "Que lugares emblematicos puedo conocer en la Ciudad de Buenos Aires?"
    },
    {
        text: "Cuales son las Principales Bibliotecas de la Ciudad de Buenos Aires?",
        value: "Cuales son las Principales Bibliotecas de la Ciudad de Buenos Aires?"
    },
    {
        text: "Cuales son los espacios culturales mas importantes en la Ciudad de Buenos Aires?",
        value: "Cuales son los espacios culturales mas importantes en la Ciudad de Buenos Aires?"
    }
];

interface Props {
    onExampleClicked: (value: string) => void;
}

export const ExampleList = ({ onExampleClicked }: Props) => {
    return (
        <ul className={styles.examplesNavList}>
            {EXAMPLES.map((x, i) => (
                <li key={i}>
                    <Example text={x.text} value={x.value} onClick={onExampleClicked} />
                </li>
            ))}
        </ul>
    );
};
