import { Example } from "./Example";

import styles from "./Example.module.css";

export type ExampleModel = {
    text: string;
    value: string;
};

const EXAMPLES: ExampleModel[] = [
    {
        text: "Qué características tiene la semilla 40R21?",
        value: "Qué características tiene la semilla 40R21?"
    },
    {
        text: "Donde se adapta la semilla 33R22?",
        value: "Donde se adapta la semilla 33R22?"
    },
    {
        text: "Cuál es la mejor época del año para sembrar semillas?",
        value: "Cuál es la mejor época del año para sembrar semillas?"
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
